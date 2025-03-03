import React from 'react'
import Image from 'next/image'
import HeroSvg from '@/imgs/hero.svg'


const LandingHeroImg = () => {
    return (
        <Image
            src={HeroSvg}
            alt="Aurum logo"
            width={750}
            height={750}
        />
    )
}

export default LandingHeroImg