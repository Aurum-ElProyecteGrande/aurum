import React from "react";
import { PieChart, Pie, Cell, ResponsiveContainer } from "recharts";

const MobileBalanceChart = ({ data, colors }) => {   
    const chartData = data.map((accountData) => ({
        name: accountData.account.displayName,
        value: Math.abs(accountData.balance),
        currency: accountData.account.currency.symbol,
    }));
    
    
    return (
        <ResponsiveContainer width="100%" height="100%">
            <PieChart>
                <Pie
                    data={chartData}
                    dataKey="value"
                    nameKey="name"
                    cx="50%" 
                    cy="50%" 
                    outerRadius={80} 
                    fill="#8884d8"
                >
                    {chartData.map((entry, index) => (
                        <Cell 
                            key={`cell-${index}`} 
                            fill={colors[index % colors.length]} 
                        />
                    ))}
                </Pie>
            </PieChart>
        </ResponsiveContainer>
    );
};

export default MobileBalanceChart;
