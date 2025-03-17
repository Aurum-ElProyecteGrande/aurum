"use client"
import { useState, useEffect } from "react";
import LandingNavbar from "@/components/landing_page/landing_nav/LandingNavbar";
import LandingHero from "@/components/landing_page/landing_hero/LandingHero";
import LandingScroll from "@/components/landing_page/landing_scroll/LandingScroll";
import LandingPrices from "@/components/landing_page/landing-prices/LandingPrices";
import LandingNewsletter from "@/components/landing_page/landing_newsletter/LandingNewsLetter";
import LandingFooter from "@/components/landing_page/landing_footer/LandingFooter";
import LandingShowcase from "@/components/landing_page/landing_showcase/LandingShowcase";
import Aos from 'aos';
import 'aos/dist/aos.css';
import AuthModal from "@/components/modal/AuthModal";
import { useRouter } from 'next/navigation';
import { fetchValidate, fetchAllCurrency } from "@/scripts/landing_page_scripts/landing_page";

export default function Home() {
  const [showModal, setShowModal] = useState(false)
  const [isSignUp, setIsSignUp] = useState(false)
  const [userInfo, setUserInfo] = useState({})
  const [currencies, setCurrencies] = useState([])
  const router = useRouter();

  const handleLinkClick = async (url) => {
    const isSuccess = await fetchValidate()

    if (isSuccess) {
      router.push(url);
    } else {
      router.push('/');
      handleModal(false)
    }
  };

  const handleModal = (signMode) => {
    setShowModal(true);
    setIsSignUp(signMode);
  }

  const handleModalRoute = async (isSignUp) => {
    if (isSignUp)
      return;

    const isSuccess = await fetchValidate()

    if (isSuccess) {
      router.push('/dashboard');
    } else {
      router.push('/');
      handleModal(false)
    }
  }

  useEffect(() => {
    Aos.init({ duration: 750 });
  }, []);

  useEffect(() => {
    const getCurrencies = async () => {
      const currencies = await fetchAllCurrency()
      setCurrencies(currencies)
    }
    getCurrencies()
  }, [])

  return (
    <>
      <LandingNavbar useModal={handleModal} userInfo={userInfo} handleLinkClick={handleLinkClick}  />
      <LandingHero handleLinkClick={handleLinkClick} />
      <LandingScroll />
      <LandingPrices />
      <LandingShowcase />
      <LandingNewsletter />
      <LandingFooter />
      {showModal && <AuthModal showModal={showModal} setShowModal={setShowModal} isSignUp={isSignUp} handleModalRoute={handleModalRoute} currencies={currencies} />}
    </>
  );
}
