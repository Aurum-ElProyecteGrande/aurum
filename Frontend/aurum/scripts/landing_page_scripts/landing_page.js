import Logo1 from '@/imgs/forbes_logo.png'
import Logo2 from '@/imgs/github_logo.png'

export const scrollInfo = [
    {
        id:1231211253453,
        img: Logo1,
        alt: "aurum logo 1"
    },
    {
        id:11222222253453,
        img: Logo2,
        alt: "aurum logo 2"
    },
    {
        id:1312532654353453,
        img: Logo1,
        alt: "aurum logo 1"
    },
    {
        id:112228978253453,
        img: Logo2,
        alt: "aurum logo 2"
    },
    {
        id:1233331253453,
        img: Logo1,
        alt: "aurum logo 1"
    },
    {
        id:1155554222253453,
        img: Logo2,
        alt: "aurum logo 2"
    }
];

const API_BASE_URL = "http://localhost:5025/";

export async function fetchTest() {
    const response = await fetch(`${API_BASE_URL}income/1`);
    if (!response.ok) {
        console.error(`Error: ${response.status} ${response.statusText}`);
        return null; // Return a fallback value if desired
    }
    return await response.json();
}