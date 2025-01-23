import LandingNavbar from "@/components/landing_page/landing_nav/LandingNavbar";
import LandingHero from "@/components/landing_page/landing_hero/LandingHero";
import LandingScroll from "@/components/landing_page/landing_scroll/LandingScroll";
import LandingPrices from "@/components/landing_page/landing-prices/LandingPrices";
import LandingNewsletter from "@/components/landing_page/landing_newsletter/LandingNewsLetter";
import LandingFooter from "@/components/landing_page/landing_footer/LandingFooter";

export default function Home() {
  return (
    <>
      <LandingNavbar />
      <LandingHero />
      <LandingScroll />
      <LandingPrices />
      <LandingNewsletter />
      <LandingFooter />
    </>
  );
}
