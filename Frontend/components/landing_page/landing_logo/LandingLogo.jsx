import React from 'react'
import Image from 'next/image'
import Logo from '@/imgs/aurum_logo.png'


const LandingLogo = () => {
    return (
        <Image
            src={Logo}
            alt="Aurum logo"
            width={151}
            height={153}
        />
    )
}

export default LandingLogo