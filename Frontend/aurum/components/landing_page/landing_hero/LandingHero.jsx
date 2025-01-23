import React from 'react'
import LandingHeroImg from '../landing_hero_img/LandingHeroImg'

const LandingHero = () => {
  return (
    <section className="landing-hero">
      <div className="landing-hero-container wrapper">
        <div className="landing-hero-left">
          <LandingHeroImg />
        </div>
        <div className="landing-hero-right">
          <h1>
          Simplify. Track. <span className='accent-text'>Gold</span>
          </h1>
          <p>
           Lorem ipsum dolor sit amet consectetur adipisicing elit. Consectetur enim adipisci, cum explicabo error nobis deleniti nemo totam doloribus exercitationem consequuntur, dolores nesciunt. Nobis fugit expedita eveniet, error hic inventore.
          </p>
          <button className="fancy-button">
            ACTION CALL TEXT
          </button>
        </div>
      </div>
    </section>
  )
}

export default LandingHero