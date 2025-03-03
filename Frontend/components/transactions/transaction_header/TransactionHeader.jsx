import React from 'react'
import { FaSearch } from "react-icons/fa";
import { CgProfile } from "react-icons/cg";

const TransactionHeader = ({onChange}) => {
  return (
    <header className="transactions-header">
        <div className="transactions-search">
          <input
            type="search"
            placeholder="Search" 
            onChange={onChange}
          />
          <FaSearch />
        </div>
        <div className="profile-pic">
          <CgProfile />
        </div>
    </header>
  )
}

export default TransactionHeader