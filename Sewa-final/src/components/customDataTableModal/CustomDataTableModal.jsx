import React from 'react'
import Modal from 'react-modal';
import { FaWindowClose } from 'react-icons/fa';
import ChangeOrderStatus from '../admin/changeTicketStatus/ChangeTicketStatus';


const CustomDataTableModal = ({ isOpen, onClose, ticketId }) => {

    
    return (
        <Modal isOpen={isOpen} onRequestClose={onClose} ariaHideApp={false}>
            <FaWindowClose size={20} onClick={onClose} />
            {<ChangeOrderStatus id={ticketId} onClose={onClose} />}
        </Modal>
    );

}

export default CustomDataTableModal
