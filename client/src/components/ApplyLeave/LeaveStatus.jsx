import LeaveStatusItem from "./LeaveStatusItem";

export default function LeaveStatus({leaveBalances}){
    return(
        <div className="leave-status flex flex-col bg-[var(--surface)] rounded-lg border border-[var(--border)] w-full lg:w-1/2">
        <div className="card-header text-md font-bold p-4 pb-0">Your available balance</div>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 w-full p-6 b ">
            {leaveBalances.map((balance, index) => (
                <LeaveStatusItem key={index} balance = {balance}/>
            ))}
        </div>
        </div>
    )
}
