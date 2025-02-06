"use client";
import React, { useState, useEffect } from "react";
import TransactionSidebar from "@/components/transactions/transaction_sidebar/TransactionSidebar";
import TransactionTable from "@/components/transactions/transaction_table/TRansactionTable";
import TransactionHeader from "@/components/transactions/transaction_header/TransactionHeader";
import { fetchTest } from "@/scripts/landing_page_scripts/landing_page";


export default function TransactionsPage() {
  const [data, setData] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage] = useState(15);
  const [search, setSearch] = useState("");
  const [filteredData, setFilteredData] = useState([])
  const [sort, setSort] = useState({key:null, direction:"ascending"});

   // Fetch data
   useEffect(() => {
    const getData = async () => {
      const fetchedData = await fetchTest();
      setData(fetchedData);
      setFilteredData(fetchedData)
    };
    getData();
  }, []);

  // Filter data when search changes
  useEffect(() => {
    let updatedFilteredData = data;

    if (search) {
      updatedFilteredData = data.filter(
        (d) =>
          d.category.name.toLowerCase().includes(search.toLowerCase()) ||
          d.subcategory?.name.toLowerCase().includes(search.toLowerCase()) ||
          d.label.toLowerCase().includes(search.toLowerCase())
      );
    }

    setFilteredData(updatedFilteredData);
  }, [search, data]);

  // Sort data when sort key or direction changes
  useEffect(() => {
    let updatedFilteredData = [...filteredData];

    if (sort.key !== null) {
      updatedFilteredData.sort((a, b) => {
        const keyParts = sort.key.split('.');
        const aValue = keyParts.reduce((acc, part) => acc && acc[part], a);
        const bValue = keyParts.reduce((acc, part) => acc && acc[part], b); 
  
        if (aValue < bValue) {
          return sort.direction === "ascending" ? -1 : 1;
        }
        if (aValue > bValue) {
          return sort.direction === "ascending" ? 1 : -1;
        }
        return 0;
      });
    }

    setFilteredData(updatedFilteredData);
  }, [sort, data]);
  

  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;

  const currentData = filteredData.slice(indexOfFirstItem, indexOfLastItem);

  const totalPages = Math.ceil(filteredData.length / itemsPerPage);

  console.log(data.length, filteredData.length, currentData.length)

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  const handleSearchChange = (e) => {
    setSearch(e.target.value);
  };

  const handleSort = (key) => {
    let direction = 'ascending';
    
    if (sort.key === key && sort.direction === 'ascending') {
      direction = 'descending';
    }
  
    setSort({ key, direction });
  };


  return (
    <section className="transactions">
      <TransactionSidebar />
      <TransactionHeader onChange={handleSearchChange} />
      <div className="transactions-container wrapper">
        <TransactionTable data={currentData} onClick={handleSort}/>
        <div className="transactions-pagination">
          <button
            className="primary-button"
            onClick={() => handlePageChange(currentPage - 1)}
            disabled={currentPage === 1}
          >
            Previous
          </button>

          <span>
            Page {currentPage} of {totalPages}
          </span>

          <button
            className="primary-button"
            onClick={() => handlePageChange(currentPage + 1)}
            disabled={currentPage === totalPages}
          >
            Next
          </button>
        </div>
      </div>
    </section>
  );
}