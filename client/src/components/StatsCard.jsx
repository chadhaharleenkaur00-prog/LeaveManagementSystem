export function StatsCard(props){
    return(
        <div className="stat-card flex flex-col gap-4 h-[200px] w-full bg-[var(--surface)] rounded-lg p-6 border border-[var(--border)] ">
            <div className="stat-icon">{props.icon}</div>
            <div className="stat-header text-sm text-[var(--ink2)]">{props.title}</div>
            <div className="stat-value text-2xl font-bold">{props.value}</div>
            <div className="bottom-text text-xs text-[var(--ink3)]">{props.description}</div>
        </div>
    )
}