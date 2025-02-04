import React from 'react'
import LandingHeroImg from '../landing_hero_img/LandingHeroImg'

const LandingHero = () => {
  return (
    <section className="landing-hero">
      <div className="landing-hero-container wrapper" data-aos="fade-left">
        <div className="landing-hero-left">
          <LandingHeroImg />
        </div>
        <div className="landing-hero-right">
          <h1>
          Simplify. Track. <span className='accent-text'>Gold</span>
          </h1>
          <p>
          Effortlessly track your expenses with Aurum—your smart companion for smarter financial decisions. Stay organized, save more, and take control of your financial future.
          </p>
          <button className="fancy-button">
          Get Started Today
          </button>
        </div>
      </div>
    </section>
  )
}

export default LandingHero