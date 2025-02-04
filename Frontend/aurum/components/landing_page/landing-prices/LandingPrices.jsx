import React from 'react'
import LandingPriceCard from '../landing-price_card/LandingPriceCard'
import { freePlanFeatures, proPlanFeatures, familyPlanFeatures } from '@/scripts/landing_page_scripts/landing_page'
import { GiFamilyHouse } from "react-icons/gi";
import { FaGift, FaTrophy } from "react-icons/fa6";


const LandingPrices = () => {
  return (
    <section id="prices" className="landing-prices" data-aos="fade-up">
      <h2>SAMPLE HEADER FOR INFO CARDS</h2>
      <div className="landing-prices-container wrapper">
        <LandingPriceCard
          img={FaGift}
          name="Free plan"
          price="0.00$"
          list = {freePlanFeatures}
        />
        <LandingPriceCard
          img={FaTrophy}
          name="Pro plan"
          price="69.99$ / year"
          list = {proPlanFeatures}
        />
        <LandingPriceCard
          img={GiFamilyHouse}
          name="Family plan"
          price="99.99$ / year"       
          list = {familyPlanFeatures}
        />
      </div>
    </section>
  )
}

export default LandingPrices