import React, { useState } from 'react'
import Loader from '../../loader/Loader'
import styles from "./ChangeTicketStatus.module.scss";
import Card from '../../card/Card';
import { toast } from 'react-toastify';
import { useDispatch } from 'react-redux';
import { updateTicket } from '../../../redux/slice/tokenSlice';


const ChangeOrderStatus = ({ id, onClose }) => {


  const [status, setStatus] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const dispatch = useDispatch();

  const [description, setDescription] = useState('');

  const handleInputChange = (event) => {
    setDescription(event.target.value);
  };

  const updateStatus = async (e, id) => {
    e.preventDefault();
    setIsLoading(true);

    try {

      setIsLoading(false);
      var payload = {
        ticketId: id,
        status: status,
        description: description
      }
      const updatedStatus = await dispatch(updateTicket(payload));
      if (updatedStatus) {       
        toast.success("Ticket status changes successfully.");
        onClose(); // Close the modal after the status update is successful       
      }
    } catch (error) {
      setIsLoading(false);
      toast.error(error.message);
    }
  };

  return (
    <>
      {isLoading && <Loader />}

      <div className={styles.status}>
        <Card cardClass={styles.card}>
          <h4>Update Status</h4>
          <form onSubmit={(e) => updateStatus(e, id)}>
            <span>
              <select
                value={status}
                onChange={(e) => setStatus(e.target.value)}
              >
                <option value="" disabled>
                  -- Choose one --
                </option>
                <option value="1">Resolved</option>
                <option value="2">Unresolved</option>
                <option value="5">InformationRequired</option>
              </select>
            </span>
            <span>
              <label> Description</label>
              <textarea
                name="description"
                required
                value={description}
                onChange={(e) => handleInputChange(e)}
                cols="30"
                rows="10"
              ></textarea>
            </span>
            <span>
              <button type="submit" className="--btn --btn-primary">
                Update Status
              </button>
            </span>
          </form>
        </Card>
      </div>
    </>
  )
}

export default ChangeOrderStatus
