import React from 'react';

const TransactionTable = ({ data, onClick }) => {
    return (
        <table className="transactions-table wrapper">
            <thead>
                <tr>
                    <th onClick={() => onClick('category.name')}>Category</th>
                    <th onClick={() => onClick('subcategory.name')}>Subcategory</th>
                    <th onClick={() => onClick('label')}>Label</th>
                    <th onClick={() => onClick('amount')}>Amount</th>
                    <th onClick={() => onClick('date')}>Date</th>
                </tr>
            </thead>
            <tbody>
                {data.map((item, index) => (
                    <tr key={index}>
                        <td>{item.category.name}</td>
                        <td>{item.subcategory?.name || "-"}</td>
                        <td>{item.label}</td>
                        <td>${item.amount.toFixed(2)}</td>
                        <td>{new Date(item.date).toLocaleDateString()}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default TransactionTable;