"use client"
import { useState ,useEffect } from "react";
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

export default function Home() {
  const [showModal, setShowModal] = useState(false)
  const [isSignUp, setIsSignUp] = useState(false)
  const [userInfo, setUserInfo] = useState({})

  const handleModal = (signMode) => {
    setShowModal(true);
    setIsSignUp(signMode);
  }

  useEffect(() => {
    Aos.init({ duration: 750 });
  }, []);

  return (
    <>
      <LandingNavbar useModal={handleModal} userInfo={userInfo}/>
      <LandingHero />
      <LandingScroll />
      <LandingPrices />
      <LandingShowcase />
      <LandingNewsletter />
      <LandingFooter />
      {showModal && <AuthModal showModal={showModal} setShowModal={setShowModal} isSignUp={isSignUp} setUserInfo={setUserInfo} />}
    </>
  );
}
