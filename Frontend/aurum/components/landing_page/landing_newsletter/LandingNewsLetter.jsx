import React from 'react'

const LandingNewsletter = () => {
  return (
    <section className="landing-newsletter">
      <div className="landing-newsletter-container wrapper">
        <h2>ACTION CALL TO SUBSCRIBE</h2>
        <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Reprehenderit laborum nam nemo, vero excepturi sit quia quasi laboriosam repellat! Quo quis impedit dolorum odio vero illum, dignissimos molestias excepturi? Temporibus.</p>
        <form action="#">
          <input
            type="email"
            placeholder="Write your email here" 
            required
          />
          <button className="primary-button">SUBSCRIBE</button>
        </form>
      </div>
    </section>
  )
}

export default LandingNewsletter