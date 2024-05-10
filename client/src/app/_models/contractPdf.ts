import { Contract } from "./contract"

export interface ContractPdf {
    id: number
    file: File | null
    filePath: string
    contractId: number
    contract: Contract
}