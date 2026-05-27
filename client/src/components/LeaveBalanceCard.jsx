import LeaveBalanceItem from "./LeaveBalanceItem";

export default function LeaveBalanceCard({leaveBalances = []}){
    return(
        <div className="leave-balance-card flex flex-col pt-2 p-6 bg-[white] rounded-2xl border border-[var(--border)] w-full">
            <div className="card-header text-md font-bold mb-4 mt-4">Leave Balances</div>
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                {leaveBalances.map((balance, index) => (
                    <LeaveBalanceItem 
                        key={index}
                        leaveType={balance.leaveType.typeName} 
                        usedDays={balance.usedDays} 
                        remainingDays={balance.remainingDays} 
                    />
                ))}
            </div>
        </div>
    )
}
