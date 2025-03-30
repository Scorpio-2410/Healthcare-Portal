CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    role_type VARCHAR(16) NOT NULL,
    first_name VARCHAR(32) NOT NULL,
    last_name VARCHAR(32) NOT NULL,
    dob DATE NOT NULL
);

CREATE TABLE HealthcareProviders (
    healthcare_provider_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    user_id INT NOT NULL UNIQUE,
    specialty VARCHAR(32) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE
);

CREATE TABLE Patients (
    patient_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    user_id INT NOT NULL UNIQUE,
    user_gender VARCHAR(16) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE
);

CREATE TABLE Appointments (
    appointment_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    patient_id INT NOT NULL,
    healthcare_provider_id INT NOT NULL,
    appointment_datetime DATETIME NOT NULL,
    appointment_status VARCHAR(16) NOT NULL CHECK (appointment_status IN ('Scheduled', 'Completed', 'Cancelled')),
    appointment_notes VARCHAR(255) NOT NULL,
    FOREIGN KEY (patient_id) REFERENCES Patients(patient_id) ON DELETE NO ACTION,
    FOREIGN KEY (healthcare_provider_id) REFERENCES HealthcareProviders(healthcare_provider_id) ON DELETE NO ACTION
);

CREATE TABLE Prescriptions (
    prescription_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    appointment_id INT NOT NULL,
    patient_id INT NOT NULL,
    healthcare_provider_id INT NOT NULL,
    medicine VARCHAR(32) NOT NULL,
    dosage INT NOT NULL,
    frequency INT NOT NULL,
    refillable BIT NOT NULL,
    FOREIGN KEY (appointment_id) REFERENCES Appointments(appointment_id) ON DELETE NO ACTION,
    FOREIGN KEY (patient_id) REFERENCES Patients(patient_id) ON DELETE NO ACTION,
    FOREIGN KEY (healthcare_provider_id) REFERENCES HealthcareProviders(healthcare_provider_id) ON DELETE NO ACTION
);

CREATE TABLE MedicalRecords (
    medical_record_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    appointment_id INT NOT NULL,
    healthcare_provider_id INT NOT NULL,
    patient_id INT NOT NULL,
    prescription_id INT NULL, -- Nullable, because not all visits require prescriptions
    diagnosis VARCHAR(255) NOT NULL,
    treatment VARCHAR(255) NOT NULL,
    FOREIGN KEY (appointment_id) REFERENCES Appointments(appointment_id) ON DELETE NO ACTION,
    FOREIGN KEY (healthcare_provider_id) REFERENCES HealthcareProviders(healthcare_provider_id) ON DELETE NO ACTION,
    FOREIGN KEY (patient_id) REFERENCES Patients(patient_id) ON DELETE NO ACTION,
    FOREIGN KEY (prescription_id) REFERENCES Prescriptions(prescription_id) ON DELETE SET NULL
);

CREATE TABLE Vaccinations (
    vaccination_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    medical_record_id INT NOT NULL,
    vaccination_name VARCHAR(32) NOT NULL,
    dose INT NOT NULL,
    vaccination_date DATE NOT NULL,
    FOREIGN KEY (medical_record_id) REFERENCES MedicalRecords(medical_record_id) ON DELETE CASCADE
);

CREATE TABLE CommunicationChannel (
    message_id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    sender_id INT NULL,
    receiver_id INT NULL,
    time_stamp DATETIME NOT NULL DEFAULT GETDATE(),
    message_text VARCHAR(255) NOT NULL
);