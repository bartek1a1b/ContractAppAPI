import { Contract } from "./contract";
import { ContractTypeTwo } from "./contractTypeTwo";

export interface ContractTypeOne {
    id: number;
    name: string;
    contracts: Contract[];
    contractTypeTwos: ContractTypeTwo[];
}
