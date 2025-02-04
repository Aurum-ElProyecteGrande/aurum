import React from 'react'
import Desktop from '@/imgs/showcase_desktop.svg'
import Mobile from '@/imgs/showcase_mobile.svg'
import Image from 'next/image'

const LandingShowcase = () => {
    return (
        <section id="views" className="landing-showcase">
            <div className="landing-showcase-container wrapper" data-aos="fade-up">
                <div className="landing-showcase-img">
                    <Image
                        src={Desktop}
                        alt="Showcase desktop image"
                        className="desktop-case"
                        width={750}
                        height={750}
                    />

                    <Image
                        src={Mobile}
                        className='mobile-case'
                        alt="Showcase mobile image"
                        width={750}
                        height={750}
                    />
                </div>
                <div className="landing-showcase-info">
                    <h2 className="desktop-case">Desktop view</h2>
                    <p className="desktop-case">Take full control of your finances with Aurum’s customizable modular dashboard. Tailor your layout to fit your needs by adding, removing, or rearranging widgets. Track spending, set goals, and view your financial progress in a way that works best for you, all in one user-friendly interface.</p>
                    <h2 className="mobile-case">Mobile view</h2>
                    <p className="mobile-case">Stay on top of your finances with Aurum’s flexible mobile dashboard. Easily customize your view by adjusting widgets to focus on what matters most, giving you quick, insightful access to your financial data wherever you are—simple, efficient, and designed for your mobile lifestyle.</p>
                </div>
            </div>
        </section>
    )
}

export default LandingShowcase