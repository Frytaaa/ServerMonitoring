<script setup lang="ts">
import {ref} from "vue";

const props = defineProps({
  chartValues: Array,
  currentValue: Number,
  title: String,
  unit: String
})
const gradient = ref(['#00c6ff', '#F0F', '#FF0'])
</script>

<template>
  <v-card
    class="mt-8 mx-auto overflow-visible"
    max-width="400"
  >
    <v-sheet
      class="v-sheet--offset mx-auto"
      color="cyan"
      elevation="12"
      max-width="calc(100% - 32px)"
      rounded="lg"
    >
      <v-sparkline
        :model-value="props.chartValues"
        :gradient="gradient"
        color="white"
        line-width="2"
        padding="16"
      ></v-sparkline>
    </v-sheet>

    <v-card-text class="pt-0">
      <div class="text-h6 font-weight-light mb-2">
        {{ props.title }} {{ props.currentValue }} {{ props.unit }}
      </div>
      <v-divider class="my-2"></v-divider>
    </v-card-text>
  </v-card>
</template>

<style scoped>
.v-sheet--offset {
  top: -24px;
  position: relative;
}
</style>
