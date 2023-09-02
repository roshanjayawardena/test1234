import React, { useEffect, useState } from 'react'
import Card from '../../card/Card';
import Loader from '../../loader/Loader';
import styles from "./Home.module.scss";
import { toast } from 'react-toastify';
import { useDispatch, useSelector } from 'react-redux';
import { allTickets, fetchAllCategories, fetchAllTicketsByServiceTypeIdAndStatus } from '../../../redux/slice/tokenSlice';
import CustomDataTable from '../../customDataTable/CustomDataTable';
import { format } from 'date-fns';
import { FaRegEye } from 'react-icons/fa';


const Home = () => {

  const dispatch = useDispatch();
  const loading = useSelector((state) => state.token.loading);
  const categories = useSelector((state) => state.token.categories);
  const [serviceTypeId, setserviceType] = useState('');
  const [status, setStatus] = useState("");
  const tickets = useSelector(allTickets);
  const [currentPage, setCurrentPage] = useState(0);
  const countPerPage = 10;

  const columns = [
    {
      name: 'Ticket ID',
      selector: (row) => row.id,
      sortable: true,
      width: '350px'
    },

    {
      name: 'Token',
      selector: (row) => row.token,
      sortable: true,
      width: '150px'
    },

    {
      name: 'Ticket Date',
      selector: (row) => mapReadableDateTime(row.createdDate),
      sortable: false,
      width: '200px'
    },

    {
      name: 'Status',
      selector: 'status',
      sortable: false,
      // cell: (row) => mapStatusToText(row.status),
      cell: (row) => (
        <div style={getStatusCellStyle(row.status)}>
          {mapStatusToText(row.status)}
        </div>
      ),
      width: '100px'
    },
  ];


  const getStatusCellStyle = (status) => {
    return {
      backgroundColor: getStatusColor(status),
      color: 'black',
    };
  };

  const mapStatusToText = (status) => {
    switch (status) {
      case 1:
        return 'Resolved';
      case 2:
        return 'Unresolved';
      case 3:
        return 'Pending';
      case 4:
        return 'InProgress';
      default:
        return 'Unknown';
    }
  };

  const mapReadableDateTime = (value) => {
    // Format the date using date-fns format function
    const formattedDate = format(new Date(value), 'yyyy-MM-dd HH:mm:ss');
    return formattedDate;
  };

  const getStatusColor = (status) => {
    switch (status) {
      case 1:
        return 'green';
      case 2:
        return 'blue';
      case 3:
        return 'orange';
      case 4:
        return 'red';
      default:
        return 'transparent';
    }
  };

  const conditionalRowStyles = [
    {
      when: (row) => true, // Always apply the click style
      style: {
        cursor: 'pointer',
      },
    },
  ];

  const fetchAllTickets = async (currentPage, serviceTypeId) => {
    try {
      await dispatch(fetchAllTicketsByServiceTypeIdAndStatus({ pageSize: countPerPage, pageNo: currentPage, serviceTypeId, status }));

    } catch (error) {
      toast.error(error.message);
    }
  };

  const handlePageChange = (page) => {
    setCurrentPage(page - 1); // Update the current page index in state
  };

  const fetchCategories = async () => {
    try {
      await dispatch(fetchAllCategories());
    } catch (error) {
      toast.error(error.message);
    }
  };

  const handleDropdownChange = (event) => {
    // Update the selected value when the dropdown changes
    setserviceType(event.target.value);
  };

  const handleClick = async (e) => {
    e.preventDefault();
    fetchAllTickets(currentPage, serviceTypeId);
  };

  useEffect(() => {
    fetchCategories();
  }, [])

  return (
    <>
      {loading && <Loader />}
      <h2> Reports</h2>
      <div className={styles.token}>
        <Card cardClass={styles.card}>
          <form onSubmit={handleClick}>
            <label>Service Category:</label>
            <select
              required
              name="categoryId"
              value={serviceTypeId}
              onChange={(e) => handleDropdownChange(e)}
            >
              <option value="" disabled>
                -- choose service category --
              </option>
              {categories.map((cat) => {
                return (
                  <option key={cat.id} value={cat.id}>
                    {cat.name}
                  </option>
                );
              })}
            </select>
            <span>
              <label>Resoloution Status:</label>
              <select
                value={status}
                onChange={(e) => setStatus(e.target.value)}
              >
                <option value="" disabled>
                  -- Choose one --
                </option>
                <option value="1">Resolved</option>
                <option value="2">Unresolved</option>
                <option value="3">Pending</option>
                <option value="4">InProgress</option>
                <option value="5">InformationRequired</option>
              </select>
            </span>
            <button className="--btn --btn-primary">
              Generate Reports
            </button>
          </form>
        </Card>
      </div>

      <CustomDataTable
        title={"Tickets"}
        data={tickets.data}
        loading={loading}
        columns={columns}
        totalRows={tickets.dataLength}
        onChange={handlePageChange} // Use the handlePageChange function to update the page index
        // onChange={page => setPage(page)}  
        conditionalRowStyles={conditionalRowStyles}
      />
    </>
  )
}

export default Home
