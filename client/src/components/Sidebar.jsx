export default function Sidebar({active, setActive, user, initials, handleLogout, navigate}){
    return(
        <div className="sidebar bg-[var(--ink)] w-[260px] min-h-screen p-0 text-white position-relative flex flex-col gap-2">
                <div className="logo flex items-center gap-3 px-[20px] py-[24px] border-[rgba(255,255,255,.08)] border-b">
                    <div className="w-8 h-8 bg-[var(--accent)] rounded-lg flex items-center justify-center text-white text-lg">🗓</div>
                    <div className="flex flex-col gap-1">
                        <div className="font-bold text-sm">LeaveTracker</div>
                        <div className="text-[var(--ink2)] text-xs">v1.0</div>
                    </div>
                </div>
                <div className="main-menu flex flex-col gap-2 px-[16px] py-[16px]">
                    <div className="main-text text-[var(--ink2)] text-xs mt-1">MAIN</div>
                    <div className="menu-items flex flex-col gap-1">
                        <div className={`group h-10 flex gap-2 items-center rounded-lg px-2 py-1 cursor-pointer ${ active=== "dashboard" ? "bg-[var(--accent)]" : "hover:bg-[rgba(255,255,255,.08)] "}`} onClick={() => {setActive("dashboard"); navigate("/dashboard");}}>
                            <svg className={`w-5 h-4 text-[rgba(255,255,255,.55)] hover:text-white ${ active=== "dashboard" ? "text-white" : "text-[rgba(255,255,255,.55)] group-hover:text-white"}`}viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.8"><rect x="3" y="3" width="7" height="7" rx="1"/><rect x="14" y="3" width="7" height="7" rx="1"/><rect x="3" y="14" width="7" height="7" rx="1"/><rect x="14" y="14" width="7" height="7" rx="1"/></svg>
                            <div className={`menu-item text-sm text-[rgba(255,255,255,.55)] ${ active=== "dashboard" ? "text-white" : "text-[rgba(255,255,255,.55)] group-hover:text-white" }`}>Dashboard</div>
                        </div>
                        <div className={`group h-10 flex gap-2 items-center rounded-lg px-2 py-1 cursor-pointer ${ active=== "apply" ? "bg-[var(--accent)]" : "hover:bg-[rgba(255,255,255,.08)] "}`} onClick={() => {setActive("apply"); navigate("/apply-leave");}}>
                            <svg className={`w-5 h-4 text-[rgba(255,255,255,.55)] hover:text-white ${ active=== "apply" ? "text-white" : "text-[rgba(255,255,255,.55)] group-hover:text-white"}`}viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.8"><path d="M12 5v14M5 12h14"/></svg>
                            <div className={`menu-item text-sm text-[rgba(255,255,255,.55)] ${ active=== "apply" ? "text-white" : "text-[rgba(255,255,255,.55)] group-hover:text-white" }`}>Apply Leave</div>
                        </div>
                        <div className={`group h-10 flex gap-2 items-center rounded-lg px-2 py-1 cursor-pointer ${ active=== "my-leaves" ? "bg-[var(--accent)]" : "hover:bg-[rgba(255,255,255,.08)] "}`} onClick={() => {setActive("my-leaves"); navigate("/my-leaves");}}>
                            <svg className={`w-5 h-4 text-[rgba(255,255,255,.55)] hover:text-white ${ active=== "my-leaves" ? "text-white" : "text-[rgba(255,255,255,.55)] group-hover:text-white"}`}viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.8"><path d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2"/></svg>
                            <div className={`menu-item text-sm text-[rgba(255,255,255,.55)] ${ active=== "my-leaves" ? "text-white" : "text-[rgba(255,255,255,.55)] group-hover:text-white" }`}>My Leaves</div>
                        </div>
                    </div>
                    {user?.designation == "Manager" && (
                        <>
                        <div className="main-text text-[var(--ink2)] text-xs mt-4">ADMIN</div>
                        
                        <div className="menu-items flex flex-col gap-1">
                            <div className={`group h-10 flex gap-2 items-center rounded-lg px-2 py-1 cursor-pointer ${ active=== "admin-portal" ? "bg-[var(--accent)]" : "hover:bg-[rgba(255,255,255,.08)] "}`} onClick={() => {setActive("admin-portal"); navigate("/admin-portal");}}>
                                <svg className={`w-5 h-4 text-[rgba(255,255,255,.55)] hover:text-white ${ active=== "admin-portal" ? "text-white" : "text-[rgba(255,255,255,.55)] group-hover:text-white"}`}viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.8"><path d="M17 21v-2a4 4 0 00-4-4H5a4 4 0 00-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 00-3-3.87M16 3.13a4 4 0 010 7.75"/></svg>
                                <div className={`menu-item text-sm text-[rgba(255,255,255,.55)] ${ active=== "admin-portal" ? "text-white" : "text-[rgba(255,255,255,.55)] group-hover:text-white" }`}>Admin Portal</div>
                            </div>
                        </div>
                        </>
                    )}
                </div>
                <div className="sidebar-footer flex items-center gap-3 border-[rgba(255,255,255,.08)] border-t mt-auto p-[16px]" >
                    <div className="user-logo w-8 h-8 bg-[var(--accent)] rounded-full flex items-center justify-center text-white text-sm">{initials}</div>
                    <div className="flex flex-col">
                        <div className="username text-sm">{user?.name}</div>
                        <div className="designation text-[var(--ink3)] text-xs">{user?.designation}</div>
                    </div>
                    <div className="logout ml-auto cursor-pointer" aria-label="Logout" title="Logout" onClick={handleLogout} >
                        <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="rgba(255,255,255,.3)" strokeWidth="1.8"><path d="M9 21H5a2 2 0 01-2-2V5a2 2 0 012-2h4M16 17l5-5-5-5M21 12H9"/></svg>
                    </div>
                </div>
            </div>
    )
}
