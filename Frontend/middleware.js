import { NextResponse } from "next/server";

export async function middleware(request) {
	const token = request.cookies.get("AuthToken");
	
	if (!token) 
		return NextResponse.redirect(new URL("/", request.nextUrl));
	
	return NextResponse.next();
}

export const config = {
	matcher: ["/dashboard", "/transactions"],
};
