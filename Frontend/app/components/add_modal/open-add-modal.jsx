import React from 'react'
import { IoIosAddCircle } from "react-icons/io";

function OpenAddModal({setIsAddModal}) {
    return (
        <div className='open-add-modal'>
            <IoIosAddCircle className='modal-button' onClick={() => setIsAddModal(true)} />
        </div>
    )
}

export default OpenAddModal