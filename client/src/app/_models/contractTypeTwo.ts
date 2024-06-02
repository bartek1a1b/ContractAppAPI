import { Contract } from "./contract";
import { ContractTypeOne } from "./contractTypeOne";

export interface ContractTypeTwo {
    id: number;
    name: string;
    contractTypeOneId: number;
    contractTypeOne: ContractTypeOne;
    contracts: Contract[];
}
