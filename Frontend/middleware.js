import { NextResponse } from "next/server";

export async function middleware(request) {
	const token = request.cookies.get("AuthToken");
	
	if (!token) 
		return NextResponse.redirect(new URL("/", request.nextUrl));

	/* const apiBaseUrl = "http://localhost:8080/user/validate";
	const apiUrl = `${request.nextUrl.origin}/api/User/validate`;

	const response = await fetch(new URL(apiBaseUrl), {
		method: "GET",
		headers: {
			Authorization: `Bearer ${token}`
		},
	});

	if (!response.ok) 
		return NextResponse.redirect(new URL("/", request.nextUrl)); */

	return NextResponse.next();
}

export const config = {
	matcher: ["/dashboard", "/transactions"],
};
