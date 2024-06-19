
class Validator {
    static isNotNullOrEmpty(value) {
        return value !== null && value !== undefined && value.trim() !== '';
    }

    static isNotScript(value) {
        const scriptPattern = /<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi;
        return !scriptPattern.test(value);
    }

    static validateTask(task) {
        const { Name, Description, StartDate, EndDate, IdProject, Status } = task;

        if (!this.isNotNullOrEmpty(Name) || !this.isNotScript(Name)) return 'Invalid Name';
        if (!this.isNotNullOrEmpty(Description) || !this.isNotScript(Description)) return 'Invalid Description';
        if (!this.isNotNullOrEmpty(StartDate)) return 'Invalid StartDate';
        if (!this.isNotNullOrEmpty(EndDate)) return 'Invalid EndDate';
        if (!this.isNotNullOrEmpty(IdProject)) return 'Invalid IdProject';
        if (!this.isNotNullOrEmpty(Status)) return 'Invalid Status';

        return null;
    }
}
