import React from 'react';
import { displayCurrency } from '@/scripts/dashboard_scripts/dashboard_scripts';

const TransactionTable = ({ data, onClick }) => {
    return (
        <table className="transactions-table wrapper">
            <thead>
                <tr>
                    <th onClick={() => onClick('label')}>Label</th>
                    <th onClick={() => onClick('category.name')}>Category</th>
                    <th onClick={() => onClick('subcategory.name')}>Subcategory</th>
                    <th onClick={() => onClick('amount')}>Amount</th>
                    <th onClick={() => onClick('date')}>Date</th>
                </tr>
            </thead>
            <tbody>
                {data.map((item, index) => (
                    <tr key={index}>
                        <td>{item.label}</td>
                        <td>{item.category.name}</td>
                        <td>{item.subcategory?.name || "-"}</td>
                        <td>{item.isExpense ? "-" : "+"} {displayCurrency(item.amount, item.currency.currencyCode)}</td>
                        <td>{new Date(item.date).toLocaleDateString()}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default TransactionTable;