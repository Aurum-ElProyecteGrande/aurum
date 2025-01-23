import { League_Spartan } from 'next/font/google';
import "./globals.scss";

const leagueSpartan = League_Spartan({
  subsets: ['latin'],
  weight: ['300', '400', '500', '600', '700', '800', '900'], //Light, Regular, Medium, SemiBold, Bold, ExtraBold, Black
});

export const metadata = {
  title: "Aurum",
  description: "World's best expense tracker app",
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>
      {children}
      </body>
    </html>
  );
}
