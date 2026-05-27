export default function LeaveBalanceItem({leaveType, usedDays, remainingDays}){
    const totalDays = usedDays + remainingDays;
    return(
        <div className="leave-item flex flex-col bg-[var(--surface2)] rounded-2xl p-4 border border-[var(--border)]">
            <div className="leave-type text-sm text-[var(--ink2)]">{leaveType}</div>
            <div className="leave-details flex items-center gap-1">
                <div className="used-days flex flex-col text-lg font-bold text-[black]">{usedDays}</div>     
                <div className="remaining-days flex flex-col text-sm text-[var(--ink3)]">/ {totalDays} days used</div>
            </div>
            <div className="progress-bar bg-[var(--ink3)] h-1 w-full rounded-full mt-2">
                <div className="progress bg-[var(--accent)] h-1 rounded-full" style={{width: `${(usedDays/totalDays)*100}%`}}></div>
            </div>
        </div>
    )
}