<template>
  <v-app>
    <v-main>
      <v-container>
        <base-graph :chart-values="temperatures" :current-value="currentTemperature" title="Temperatur" unit="°C" />
        <base-graph :chart-values="humiditys" :current-value="currentHumidity" title="Feuchtigkeit" unit="%"/>
        <v-card  class="mt-8 mx-auto overflow-visible"
                  max-width="400">
          <v-card-title>NFC-Scanner</v-card-title>
          <v-card-text>
            <p>NFC Status: {{ nfcStatusText() }}</p>
            <v-btn @click="changeNfcMode()">Change Mode</v-btn>
            <p>NFC TagId: {{ nfcTag.tagId }}</p>
            <p>NFC TagType: {{ nfcTag.tagType }}</p>

          </v-card-text>
        </v-card>
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import * as signalR from "@microsoft/signalr";
import {onMounted, ref} from "vue";
import BaseGraph from "@/components/BaseGraph.vue";


const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:5001/hub")
  .build();

onMounted(() => {
  connection.start().catch(function (err) {
    return console.error(err.toString());
  });
  connection.on("UpdateTemperature", (value) => {
    temperatures.value.push(value);
    currentTemperature.value = value;
    if (temperatures.value.length > 60) {
      temperatures.value.shift();
    }
  })

  connection.on("UpdateHumidity", (value) => {
    humiditys.value.push(value);
    currentHumidity.value = value;
    if (humiditys.value.length > 60) {
      humiditys.value.shift();
    }
  })
});

const temperatures = ref(Array(60).fill(0))
const currentTemperature = ref(0)

const humiditys = ref(Array(60).fill(0)) // Initial array with 60 zeros
const currentHumidity = ref(0)

// OFF = 0, READER = 3 -> there also another but we are not interested in them in our case
const nfcStatus = ref(0)
const nfcStatusText = () => {
  switch (nfcStatus.value) {
    case 0:
      return "NFC ist ausgeschaltet"
    case 3:
      return "NFC ist im Lesemodus"
    default:
      return "Unbekannter Status"
  }
}

const changeNfcMode = () => {
  console.log("got called after button press")
  console.log(nfcStatus.value)
  connection.send("ChangeNfcMode", nfcStatus);
}

connection.on("NFCModeChanged", (value) => {
  console.log(value)
  nfcStatus.value = value;
})

connection.on("SendNfcTag", (tagId, tagType) => {
  nfcTag.value.tagId = tagId;
  nfcTag.value.tagType = tagType;
})

const nfcTag = ref({
  tagId: "",
  tagType: ""
})
</script>

