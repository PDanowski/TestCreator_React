import { Question } from "./Question";
import { TestAttemptAnswer } from "./TestAttemptAnswer";

export interface TestAttemptEntry {
    IsMultitipleChoise: boolean;
    Question: Question;
    Answers: TestAttemptAnswer[];
}