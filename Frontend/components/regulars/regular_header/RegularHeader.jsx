import React, { useState } from 'react';
import { FaSearch } from 'react-icons/fa';

const RegularHeader = ({ onSearchChange, onStartDateChange, startDate, accountOptions, handleCheckboxChange, isEditing, handleEditing }) => {
  const [accountMenuVisible, setAccountMenuVisible] = useState(false);


  const toggleAccountMenu = () => {
    setAccountMenuVisible((prevVisible) => !prevVisible);
  };

  return (
    <header className="transactions-header">
      <div className="transactions-date">
        <input
          type="date"
          placeholder="Start Date"
          value={startDate}
          onChange={onStartDateChange}
        />
      </div>
      <div className="transactions-search">
        <input
          type="search"
          placeholder="Search"
          onChange={onSearchChange}
        />
        <FaSearch />
      </div>
      <div className="transactions-accounts">
        <button className="transparent-button" onClick={toggleAccountMenu}>Accounts</button>
        {accountMenuVisible && (
          <div className="transactions-accounts-menu">
            <ul>
              {accountOptions.map((option) => (
                <li key={option.accountId}>
                  <label>
                    <input
                      type="checkbox"
                      checked={option.checked}
                      onChange={() => {
                        handleCheckboxChange(option.accountId)
                        toggleAccountMenu()
                      }}
                    />
                    {option.displayName}
                  </label>
                </li>
              ))}
            </ul>
          </div>
        )}
      </div>
      <button className={`${isEditing ? "accent-button" : "transparent-button"} regular`} onClick={() => handleEditing()}>{isEditing ? "Save" : "Edit"}</button>
    </header>
  );
};

export default RegularHeader;
