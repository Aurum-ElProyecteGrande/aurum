import React from 'react'
import Image from 'next/image'
import { scrollInfo } from '@/scripts/landing_page_scripts/landing_page.js'

const LandingScroll = () => {
    return (
        <section className="landing-scroll">
            <div className="landing-scroll-container wrapper" data-aos="zoom-in">
                {
                    scrollInfo.map(({ id, img, alt }) => (
                        <Image
                            key={id}
                            src={img}
                            alt={alt}
                            width={500}
                            height={500}
                        />
                    ))
                }
            </div>
        </section>
    )
}

export default LandingScroll