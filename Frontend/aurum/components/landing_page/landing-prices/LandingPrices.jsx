import React from 'react'
import LandingPriceCard from '../landing-price_card/LandingPriceCard'
import Logo1 from '@/imgs/aurum_logo_1.png'
import Logo2 from '@/imgs/aurum_logo_2.png'

const LandingPrices = () => {
  return (
    <section className="landing-prices">
      <h2>SAMPLE HEADER FOR INFO CARDS</h2>
      <div className="landing-prices-container wrapper">
        <LandingPriceCard
          img={Logo1}
          name="Option A"
          price="111"
        />
        <LandingPriceCard
          img={Logo2}
          name="Option B"
          price="333"
        />
        <LandingPriceCard
          img={Logo1}
          name="Option C"
          price="222"
        />
      </div>
    </section>
  )
}

export default LandingPrices