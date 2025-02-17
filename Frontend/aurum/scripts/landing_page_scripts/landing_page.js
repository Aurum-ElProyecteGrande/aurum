import Forbes from "@/imgs/forbes_logo.png";
import Github from "@/imgs/github_logo.png";
import GooglePay from "@/imgs/google_pay_logo.png";
import Mastercard from "@/imgs/mastercard_logo.png";
import OtpBank from "@/imgs/otp_bank_logo.png";
import Mit from "@/imgs/mit_logo.png";

export const scrollInfo = [
	{
		id: 1,
		img: Forbes,
		alt: "ForbesLogo",
	},
	{
		id: 2,
		img: Github,
		alt: "GithubLogo",
	},
	{
		id: 3,
		img: GooglePay,
		alt: "GooglePayLogo",
	},
	{
		id: 4,
		img: Mastercard,
		alt: "MastercardLogo",
	},
	{
		id: 5,
		img: OtpBank,
		alt: "OtpBankLogo",
	},
	{
		id: 6,
		img: Mit,
		alt: "MitLogo",
	},
];

export const freePlanFeatures = [
	"Track up to 50 transactions",
	"Basic expense categorization",
	"Simple budget tracker",
	"Limited reporting tools",
];

export const proPlanFeatures = [
	"Unlimited transactions and categories",
	"Advanced reporting and analytics",
	"Customizable dashboards and widgets",
	"Goal setting and progress tracking",
	"Priority support",
];

export const familyPlanFeatures = [
	"Multiple user access (family members, partners)",
	"Shared expense tracking and goals",
	"Customizable shared budgets and categories",
	"Family-wide progress tracking and reports",
	"Collaborative goal setting",
];


export async function fetchTest() {
	const response = await fetch(`/api/expenses/13?userid=10`);
	if (!response.ok) {
		console.error(`Error: ${response.status} ${response.statusText}`);
		return null; // Return a fallback value if desired
	}
	else
		console.log("All good")
	return await response.json();
}
