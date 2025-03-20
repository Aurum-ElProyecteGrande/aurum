import React from 'react';
import { displayCurrency } from '@/scripts/dashboard_scripts/dashboard_scripts';

const RegularCards = ({ data }) => {
    return (
        <div className="transactions-cards">
            {data.map((item, index) => (
                <div className="transactions-card" key={index}>
                    <div className="transactions-card-header">
                        <h3>{item.label}</h3>
                        <span>
                            {new Date(item.startDate).toLocaleDateString()}
                        </span>
                    </div>
                    <div className="transactions-card-body">
                        <p>
                            <strong>Category:</strong> {item.category.name}
                        </p>
                        {item.subcategory && <p>
                                <strong>Subcategory:</strong> {item.subcategory?.name || "N/A"}
                            </p>}
                        <p>
                            <strong>Amount:</strong> <span className={item.isExpense ? "negative" : "positive"}>{item.isExpense ? "-" : "+"} {displayCurrency(item.amount, item.currency.currencyCode)}</span>
                        </p>
                        <p>
                        <strong>Regularity:</strong> {item.regularity}
                        </p>
                    </div>
                    <div className="transactions-card-footer">
                        <span>
                            <strong>Account:</strong> {item.accountName}
                        </span>
                    </div>
                </div>
            ))}
        </div>
    );
};

export default RegularCards;
