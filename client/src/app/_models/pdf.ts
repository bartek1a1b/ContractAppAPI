import { AnnexToTheContract } from "./annexToTheContract";

export interface Pdf {
    id: number
    file: File | null
    filePath: string
    annexToTheContractId: number
    annexToTheContract: AnnexToTheContract
}