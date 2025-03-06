import React from 'react'

function InfoToast({ toastText, isToast, setIsToast, toastType }) {
    return (
        <div className={`toast ${toastType} ${isToast ? "show" : ""}`}>
            <div className='toast-body'>
                <p className='toast-text'>
                    {toastText}
                </p>
                <div className='buttons-container'>
                    <button className={toastType} onClick={() => setIsToast(false)}>Ok</button>
                </div>
            </div>
            {isToast && <div className={`toast-progress ${toastType}`}></div>}
        </div>
    )
}

export default InfoToast