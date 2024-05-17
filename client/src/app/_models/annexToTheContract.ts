import { Contract } from "./contract"
import { Pdf } from "./pdf";

export interface AnnexToTheContract {
    id: number
    annexNumber: number
    name: string
    dateOfConclusion: string
    description: string
    contractor: string
    signatory: string
    hasPdf: boolean
    contract: Contract
    pdfs: Pdf[]
  }