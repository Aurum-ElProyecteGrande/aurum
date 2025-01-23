import React from 'react'
import Image from 'next/image'

const LandingPriceCard = ({img, name, price}) => {
  return (
    <div className="landing-prices-card">
        <Image
                    src={img}
                    alt={name}
                    width={500}
                    height={500}
                />
        <h3>{name}</h3>
        <span>{price}</span>
        <button className="accent-button">
                ACTION CALL
        </button>
    </div>
  )
}

export default LandingPriceCard