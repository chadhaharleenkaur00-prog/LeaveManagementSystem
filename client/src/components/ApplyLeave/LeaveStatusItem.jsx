export default function LeaveStatusItem({balance}){
    return(
        <div className="flex flex-col bg-[var(--surface2)] rounded-2xl p-4 border border-[var(--border)] h-20">
            <div className="leave-type text-sm text-[var(--ink2)]">{balance.leaveType.typeName}</div>
            <div className="flex items-end gap-1">
                <div className="text-lg text-[var(--accent)] font-bold">{balance.remainingDays} </div>
                <div className="text-sm text-[var(--ink3)] mb-0.5"> days left</div>
            </div>
        </div>
    )
}