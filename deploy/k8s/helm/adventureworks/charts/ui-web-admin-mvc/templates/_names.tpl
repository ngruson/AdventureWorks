{{- define "suffix-name" -}}
{{- if .Values.app.name -}}
{{- .Values.app.name -}}
{{- else -}}
{{- .Release.Name -}}
{{- end -}}
{{- end -}}

{{- define "pathBase" -}}
{{- if .Values.inf.k8s.suffix -}}
{{- $suffix := include "suffix-name" . -}}
{{- printf "%s-%s"  .Values.pathBase $suffix -}}
{{- else -}}
{{- .Values.pathBase -}}
{{- end -}}
{{- end -}}