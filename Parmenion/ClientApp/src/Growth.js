import React, { useState } from "react";
import * as _ from "lodash";
import {
  Row,
  Col,
  Form,
  FormGroup,
  Label,
  Input,
  Button,
  InputGroup,
  InputGroupAddon,
  InputGroupText,
  Alert,
} from "reactstrap";
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  Legend,
  ResponsiveContainer,
  CartesianGrid,
  Tooltip,
} from "recharts";
import { calculateGrowth, saveContributions } from "./api";

const defaultResult = [{ highGrowth: 100 }];

const submitGrowthForm =
  (setResult, setError, setCalculating) => async (form) => {
    setError(null);

    try {
      setCalculating(true);
      const result = await calculateGrowth(form);
      setResult(result.data);
    } catch (e) {
      setError("Failed to calculate growth.  " + e);
      setResult(defaultResult);
    } finally {
      setCalculating(false);
    }
  };

const submitContributions = (setError, setSaving) => async (form) => {
  setError(null);

  try {
    setSaving(true);
    await saveContributions(form);
  } catch (e) {
    setError("Failed to save.  " + e);
  } finally {
    setSaving(false);
  }
};

export default () => {
  const [growth, setGrowth] = useState(defaultResult);
  const [error, setError] = useState(null);
  const [calculating, setCalculating] = useState(false);
  const [saving, setSaving] = useState(false);

  const submitForm = async (e) => {
    e.preventDefault();
    const formValues = Object.fromEntries(new FormData(e.target));

    if (e.nativeEvent.submitter.value === "calculate") {
      await submitGrowthForm(setGrowth, setError, setCalculating)(formValues);
    } else if (e.nativeEvent.submitter.value === "save") {
      await submitContributions(setError, setSaving)(formValues);
    }
  };

  return (
    <>
      <Row>
        <Col>
          <h1>Growth Calculator</h1>
        </Col>
      </Row>
      <Row>
        <Col md={3}>
          <Form onSubmit={submitForm}>
            <Row form>
              <FormGroup>
                <Label for="fundId">Fund</Label>
                <Input name="fundId" id="fundId" />
              </FormGroup>
              <FormGroup>
                <Label for="yearsToInvest">Years to invest</Label>
                <Input name="yearsToInvest" id="yearsToInvest" />
              </FormGroup>
              <FormGroup>
                <Label for="initialInvestment">Initial investment</Label>
                <InputGroup>
                  <InputGroupAddon addonType="prepend">
                    <InputGroupText>&pound;</InputGroupText>
                  </InputGroupAddon>
                  <Input name="initialInvestment" id="initialInvestment" />
                </InputGroup>
              </FormGroup>
              <FormGroup>
                <Label for="monthlyInvestment">Monthly investment</Label>
                <InputGroup>
                  <InputGroupAddon addonType="prepend">
                    <InputGroupText>&pound;</InputGroupText>
                  </InputGroupAddon>
                  <Input name="monthlyInvestment" id="monthlyInvestment" />
                </InputGroup>
              </FormGroup>
              <FormGroup>
                <Button
                  type="submit"
                  name="action"
                  value="calculate"
                  disabled={calculating}
                >
                  Calculate
                </Button>
              </FormGroup>
              {growth && growth[1] && (
                <>
                  <FormGroup>
                    <InputGroup>
                      <InputGroupAddon addonType="prepend">
                        <InputGroupText>Email</InputGroupText>
                      </InputGroupAddon>
                      <Input name="emailAddress" />
                    </InputGroup>
                  </FormGroup>
                  <FormGroup>
                    <Button
                      type="submit"
                      name="action"
                      value="save"
                      disabled={saving}
                    >
                      Save for Later
                    </Button>
                  </FormGroup>
                </>
              )}
            </Row>
          </Form>
          {error && (
            <Row>
              <Alert>{error}</Alert>
            </Row>
          )}
        </Col>
        <Col>
          <ResponsiveContainer height={400} width="100%">
            <LineChart
              data={growth}
              margin={{ top: 30, bottom: 30, left: 30, right: 30 }}
            >
              <XAxis
                dataKey="monthOfInvestment"
                label={{
                  value: "Month",
                  position: "bottom",
                }}
                interval={Math.ceil(
                  document.getElementById("yearsToInvest")?.value || 0 / 12
                )}
              />
              <YAxis
                allowDecimals={false}
                label={{
                  value: "Value (GBP)",
                  offset: -5,
                  angle: -90,
                  position: "insideLeft",
                }}
                domain={[
                  () => document.getElementById("initialInvestment").value,
                  () => Math.ceil(_.last(growth).highGrowth),
                ]}
              />
              <CartesianGrid />
              <Tooltip />
              <Legend />
              <Line
                dot={false}
                type="monotone"
                dataKey="invested"
                stroke="blue"
                label="Invested"
              />
              <Line
                dot={false}
                dataKey="lowGrowth"
                stackId="wide"
                stroke="pink"
                label="Low"
              />
              <Line
                dot={false}
                dataKey="mediumGrowth"
                stackId="wide"
                stroke="red"
                label="Medium"
              />
              <Line
                dot={false}
                dataKey="highGrowth"
                stackId="narrow"
                stroke="orange"
                label="High"
              />
            </LineChart>
          </ResponsiveContainer>
        </Col>
      </Row>
    </>
  );
};
