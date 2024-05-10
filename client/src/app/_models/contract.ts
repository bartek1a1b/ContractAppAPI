import { ContractTypeOne } from "./contractTypeOne"
import { ContractTypeTwo } from "./contractTypeTwo"
import { ContractPdf } from "./contractPdf"
import { AnnexToTheContract } from "./annexToTheContract"


export interface Contract {
    id: number
    contractNumber: number
    name: string
    typeNameOne: string
    typeNameTwo: string
    dateOfConclusion: string
    description: string
    value: number
    contractor: string
    signatory: string
    hasPdf: boolean

    contractTypeOne: ContractTypeOne
    contractTypeTwo: ContractTypeTwo
    contractPdfs: ContractPdf[]
    annexToTheContract: AnnexToTheContract[]
  }