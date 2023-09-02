import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { allTickets, fetchAllCategories, fetchAllTicketsByServiceTypeId, updateTicketStatus } from '../../../redux/slice/tokenSlice';
import { toast } from 'react-toastify';
import Loader from '../../loader/Loader';
import Card from '../../card/Card';
import styles from "./Home.module.scss";
import { FaRegEye } from 'react-icons/fa';
import { format } from 'date-fns';
import CustomDataTable from '../../customDataTable/CustomDataTable';
import CustomDataTableModal from '../../customDataTableModal/CustomDataTableModal';
import { ResoloutionStatusEnum } from '../../../enums/ResoloutionStatus';

const Home = () => {
    const dispatch = useDispatch();
    const loading = useSelector((state) => state.token.loading);
    const categories = useSelector((state) => state.token.categories);
    const token = useSelector((state) => state.token.token);
    const [serviceTypeId, setserviceType] = useState('');

    const [currentPage, setCurrentPage] = useState(0);
    const [orderItemsModalOpen, setOrderItemsModalOpen] = useState(false);
    const [selectedTicketId, setSelectedTicketId] = useState(0);

    const countPerPage = 10;
    const tickets = useSelector(allTickets);



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
            cell: (row) => (
                <div style={getStatusCellStyle(row.status)}>
                    {mapStatusToText(row.status)}
                </div>
            ),
            width: '100px'
        },
        {
            name: "Actions",
            selector: 'actions',
            button: true,
            width: '100px',
            cell: (row) => (
                <FaRegEye size={20} onClick={(e) => handleButtonClick(e, row.id)} />
            ),
        }
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

    const handlePageChange = (page) => {
        setCurrentPage(page - 1); // Update the current page index in state
    };

    const handleButtonClick = async (e, id) => {

        e.preventDefault();
        // Get the order items for the clicked order (You will need to fetch order items from the API based on the order ID)
        try {
            setSelectedTicketId(id)
            var payload = {
                ticketId: id,
                status: ResoloutionStatusEnum.InProgress
            }
            const updatedStatus = await dispatch(updateTicketStatus(payload));
            if (updatedStatus) {
                toast.success("Ticket status changes to IN Progress successfully");
            }
            setOrderItemsModalOpen(true);
        } catch (error) {
            toast.error(error.message);
        }
    };

    const handleDropdownChange = (event) => {
        // Update the selected value when the dropdown changes
        setserviceType(event.target.value);
    };



    const fetchCategories = async () => {
        try {
            await dispatch(fetchAllCategories());
        } catch (error) {
            toast.error(error.message);
        }
    };

    const fetchAllTickets = async (currentPage, serviceTypeId) => {
        try {
            await dispatch(fetchAllTicketsByServiceTypeId({ pageSize: countPerPage, pageNo: currentPage, serviceTypeId }));
           
        } catch (error) {
            toast.error(error.message);
        }
    };

    useEffect(() => {
        fetchCategories();
    }, [])


    const handleClick = async (e) => {
        e.preventDefault();
        fetchAllTickets(currentPage, serviceTypeId);
    };

    return (

        <>
            {loading && <Loader />}
            <h2> View All Tickets</h2>
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
                        <button className="--btn --btn-primary">
                            View Tickets
                        </button>
                    </form>
                    <h1>{token}</h1>
                </Card>
            </div>

            <CustomDataTable
                title={"Tickets"}
                data={tickets.data}
                loading={loading}
                columns={columns}
                totalRows={tickets.dataLength}
                onChange={handlePageChange} // Use the handlePageChange function to update the page index              
                conditionalRowStyles={conditionalRowStyles}
            />

            <CustomDataTableModal
                isOpen={orderItemsModalOpen}
                onClose={() => setOrderItemsModalOpen(false)}
                ticketId={selectedTicketId}
            />
        </>
    )
}

export default Home
