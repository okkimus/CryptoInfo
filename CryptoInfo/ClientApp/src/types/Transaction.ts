interface Transaction {
    hash: string
    timestamp: Date
    from: string
    to: string
    valueIn: number
    valueOut: number
    // Transfers: Array<Transfer> 
    external: boolean
}

export default Transaction