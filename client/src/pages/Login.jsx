import { useState } from "react";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

export default function Login(){
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const [loader, setLoader] = useState(false);
    const navigate = useNavigate();

    const handleLogin = async () => {
    setError("");

    // Validation - before hitting the API, never waste an API call on empty data!

    if (!email || !password) {
        setError("Please enter both email and password.");
        return;
    }

    //regex pattern - one or more non-whitespace characters, followed by an '@' symbol, followed by one or more non-whitespace characters, a dot, and one or more non-whitespace characters
    if(!/\S+@\S+\.\S+/.test(email)){
        setError("Please enter a valid email address.");
        return;
    }
    //"TRY to run this code..." If anything fails → jump to catch block
    try {
        setLoader(true);   
        console.log("LOGIN STARTED");

        const response = await fetch(
            "http://localhost:5155/api/Auth/login",
            {
                method: "POST",

                headers: {
                    "Content-Type": "application/json",
                },

                body: JSON.stringify({
                    email,
                    password,
                }),
            }
        );

        console.log("RESPONSE RECEIVED");
        toast.success("Login successful!");

        if (!response.ok) {
            throw new Error("Invalid email or password");
        }

        const data = await response.json();

        console.log(data);

        localStorage.setItem("token", data.token);
        localStorage.setItem("user", JSON.stringify(data)); //localStorage can only store strings, so we convert the user object to a JSON string before storing it

        navigate("/dashboard");

    } catch (error) {

        console.log("ERROR:");
        console.log(error);
        setError(error.message);

    }
    finally {
        setLoader(false);
    }
};

    return(
        <div className="bg-[var(--bg)] min-h-screen flex items-center justify-center">
            <div className="login-container bg-white w-[400px] rounded-2xl p-[40px] shadow-lg border-[var(--border)] border">
                <div className="header flex items-center gap-3">
                    <div className="w-10 h-10 bg-[var(--accent)] rounded-xl flex items-center justify-center text-white text-2xl">🗓</div>
                    <div>
                        <div className="font-bold text-l">LeaveTracker</div>
                        <div className="text-[var(--ink3)] text-xs">Leave Management</div>
                    </div>
                </div>
                <div className="greeting-text mt-8 mb-6">
                    <div className="text-xl font-bold">Welcome back</div>
                    <div className="text-[var(--ink2)] text-sm">Sign in to manage your leave requests.</div>
                </div>
                <div className="form">
                    <label className="text-sm text-[var(--ink2)]">Email address</label>
                    <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} className="w-full h-10 p-3 text-sm rounded-lg border-[var(--border)] border mt-1 mb-4 outline-none" placeholder="you@company.com"/>

                    <label className="text-sm text-[var(--ink2)]">Password</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} className="w-full h-10 p-3 text-sm rounded-lg border-[var(--border)] border mt-1 mb-6 outline-none" placeholder="••••••••"/>
                </div>
                {
                        error && (
                        <div className="text-red-500 text-sm mb-4">
                        {error}
                        </div>
                    )
                }
                <div className="sign-in-button">
                    <button onClick={handleLogin} className="w-full h-10 bg-[var(--accent)] text-white text-sm rounded-lg font-medium cursor-pointer" disabled={loader}>
                        {loader ? "Signing in..." : "Sign In →"}
                    </button>
                </div>
                <div className="footer flex justify-center">
                    <div className="text-xs text-[var(--ink3)] mt-6">Forgot your password? <a href="#" className="text-[var(--accent)] font-medium">Reset it</a></div>
                </div>
            </div>
        </div>
    )
}