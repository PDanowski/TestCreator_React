import { TestAttemptEntry } from "./TestAttemptEntry";

export interface TestAttempt {
    TestId: number;
    Title: string;
    TestAttemptEntries: TestAttemptEntry[];
}