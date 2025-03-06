import React, { useEffect, useState } from 'react'
import { IoIosArrowDown } from "react-icons/io";

//options: []string
function InputWithOptions({ options, inputValue, setInputValue }) {

    const [isOpen, setIsOpen] = useState(false)

    const handleSuggestionClick = (value) => {
        setInputValue(value)
        setIsOpen(false)
    }

    useEffect(() => {
        const handleClose = (e) => {
            if (e.target.id !== "arrow") setIsOpen(false)
        }
        window.addEventListener("click", handleClose)
        return () => {
            window.removeEventListener("click", handleClose);
        };
    }, [])



    return (
        <div className="sub-cat input">
            <input type='text' className='input' value={inputValue} placeholder='Add subcategory' onChange={(e) => setInputValue(e.target.value)} />
            <IoIosArrowDown id="arrow" className='arrow' onClick={() => setIsOpen(!isOpen)} />
            {isOpen &&
                <ul className='suggestions'>
                    {options[0] && options.map(o => (
                        <li className="suggestion" key={o.name} onClick={() => handleSuggestionClick(o.name)}>{o.name}</li>
                    ))}
                </ul>
            }
        </div>
    )
}

export default InputWithOptions