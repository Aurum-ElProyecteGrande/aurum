import React, { useState } from 'react';
import { regularity } from '@/scripts/dashboard_scripts/dashboard_scripts';

const RegularitySelector = ({ onSelect }) => {
    const [selectedRegularity, setSelectedRegularity] = useState('');

    const handleChange = (e) => {
        setSelectedRegularity(e.target.value);
        onSelect(e.target.value);
    };

    return (
        <div className="regularity-selector">
            <h3 className="regularity-title">Select Regularity</h3>
            <div className="regularity-options">
                <div className="regularity-option">
                    <input
                        type="radio"
                        value="None"
                        checked={selectedRegularity === "None"}
                        onChange={handleChange}
                        className="regularity-input"
                    />
                    <label >
                        None
                    </label>
                </div>

                {Object.values(regularity).map((regularityOption, index) => (
                    <div key={index} className="regularity-option">
                        <input
                            type="radio"
                            value={regularityOption}
                            checked={selectedRegularity === regularityOption}
                            onChange={handleChange}
                            className="regularity-input"
                        />  
                        <label className="regularity-option">
                            {regularityOption}
                        </label>
                    </div>

                ))}
            </div>
        </div>
    );
};


export default RegularitySelector;
