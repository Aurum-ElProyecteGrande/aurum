import React from 'react';
import { displayCurrency } from '@/scripts/dashboard_scripts/dashboard_scripts';

const RegularsTable = ({ data, onClick, isEditing, handleSelectChange }) => {
    return (
        <table className="transactions-table wrapper">
            <thead>
                <tr>
                    <th onClick={() => onClick('label')}>Label</th>
                    <th onClick={() => onClick('category.name')}>Category</th>
                    <th onClick={() => onClick('subcategory.name')}>Subcategory</th>
                    <th onClick={() => onClick('amount')}>Amount</th>
                    <th>Date</th>
                    <th>Regularity</th>
                    <th>Account</th>
                </tr>
            </thead>
            <tbody>
                {data.map((item, index) => (
                    <tr key={index}>
                        <td>{item.label}</td>
                        <td>{item.category.name}</td>
                        <td>{item.subcategory?.name || "-"}</td>
                        <td>{item.isExpense ? "-" : "+"} {displayCurrency(item.amount, item.currency.currencyCode)}</td>
                        <td>{new Date(item.startDate).toLocaleDateString()}</td>
                        {isEditing ? (
                            <td>
                                <select
                                    value={item.regularity}
                                    onChange={(e) => handleSelectChange(index, e.target.value)}
                                >
                                    <option value="Daily">Daily</option>
                                    <option value="Weekly">Weekly</option>
                                    <option value="Monthly">Monthly</option>
                                </select>
                            </td>
                        ) : (
                            <td>{item.regularity}</td>
                        )}
                        <td>{item.accountName}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default RegularsTable;