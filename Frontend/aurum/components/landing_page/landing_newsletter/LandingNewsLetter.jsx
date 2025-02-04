import React from 'react'

const LandingNewsletter = () => {
  return (
    <section className="landing-newsletter">
      <div className="landing-newsletter-container wrapper" data-aos="fade-down">
        <h2>Take Control of Your Finances Today</h2>
        <p>Experience the power of seamless expense tracking with Aurum. Whether you're managing personal finances or budgeting for a family, our platform is designed to give you full control over your spending. Stay on top of your goals with real-time insights, secure management, and a user-friendly interface. Ready to simplify your financial life?.</p>
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