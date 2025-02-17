import React from 'react'
import Image from 'next/image'

const LandingPriceCard = ({ img: Img, name, price, list = [] }) => {
  return (
    <div className="landing-prices-card">
      <Img size={30} />
      <h3>{name}</h3>
      <ul>
        {list.map((listText, index) => <li key={index}>{listText}</li>)}
      </ul>
      <span>{price}</span>
      <button className="accent-button">
      Start Your Journey
      </button>
    </div>
  );
};

export default LandingPriceCard