import { useNavigate } from "react-router-dom"
export default function Navbar({pageName}){
    const navigate = useNavigate();
    return(
        <div className="navbar bg-[white] flex items-center  h-16 w-full px-6">
            <div className="header text-md font-bold mr-auto">{pageName}</div>
            <div className="date text-sm text-[var(--ink3)]">{new Date().toLocaleDateString()}</div>

            <div className="apply-leave-button flex items-center justify-center bg-[var(--accent)] text-white px-2 py-1 rounded-lg ml-4 cursor-pointer h-8">
                <svg className="w-5 h-4 text-[white]" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.8"><path d="M12 5v14M5 12h14"/></svg>
                <div className="text-xs text-[white] font-semibold cursor-pointer" onClick={() => navigate("/apply-leave")}>
                    Apply leave
                </div>
            </div>
        </div>
    )
}