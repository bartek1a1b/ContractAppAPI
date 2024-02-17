import { ContractTypeOne } from "./contractTypeOne"
import { ContractTypeTwo } from "./contractTypeTwo"


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
    pdf: string
    contractTypeOne: ContractTypeOne
    contractTypeTwo: ContractTypeTwo
  }